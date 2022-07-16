import { useCallback, useState } from 'react';
import useAuth from './useAuth';
import axios from 'axios';

const useRefreshToken = () => {
  const { auth, setAuth } = useAuth();
  const { REACT_APP_CUSTOM_URL } = process.env;
  const obj = { token: auth.token, refreshToken: auth.refreshToken };

  const refreshTokenRequest = useCallback(async () => {
    try {
      const response = await axios.put(
        `https://${REACT_APP_CUSTOM_URL}/api/v1/User/Refresh`,
        obj
      );
      if (response.status == '200') {
        setAuth((prevState) => {
          return { ...prevState, token: response.token };
        });
        return response;
      } else {
        throw Error(response.status);
      }
    } catch (err) {
      console.log(err);
    }
  }, [REACT_APP_CUSTOM_URL]);
  return { refreshTokenRequest };
};

export default useRefreshToken;
