import axios from 'axios';
import { useCallback } from 'react';
import useAuth from '../../../hooks/useAuth';

const useProfileInfo = () => {
  const { REACT_APP_CUSTOM_URL } = process.env;
  const { auth, setAuth } = useAuth();

  const tokenRequest = `Bearer ${auth.token}`;

  const getInfo = useCallback(async () => {
    try {
      const response = await axios.get(
        `https://${REACT_APP_CUSTOM_URL}/api/v1/User/GetInfo`,
        {
          headers: {
            'Authorization': tokenRequest,
          },
        }
      );
      if (response.status == '200') {
        setAuth((prevState) => {
          return { ...prevState, userName: response.data.userName };
        });
        return response.data;
      } else {
        console.log(response.status, 'sg tkven');
      }
    } catch (err) {
      console.log(err, 'shecdoma');
    }
  }, [REACT_APP_CUSTOM_URL]);


  const updateMyInfo = useCallback(
    async (userData) => {
      try {
        const response = await axios.put(
          `https://${REACT_APP_CUSTOM_URL}/api/v1/User/UpdateInfo`,
          userData,
          {
            headers: {
              'Authorization': tokenRequest,
            },
          }
        );
        if (response.status == '200') {
          alert('changes saved')
          return response.data;
        } else {
          console.log(response.status, 'some error occured');
        }
      } catch (err) {
        console.log(err, 'error sending request');
      }
    },
    [REACT_APP_CUSTOM_URL]
  );

  return { getInfo, updateMyInfo };
};

export default useProfileInfo;
