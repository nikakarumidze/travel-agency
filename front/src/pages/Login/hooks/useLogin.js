import { useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import useAuth from '../../../hooks/useAuth';
import axios from 'axios';

const useLogin = () => {
  const { setAuth } = useAuth();
  const navigate = useNavigate();
  const { REACT_APP_CUSTOM_URL } = process.env;

  const LoginHandler = useCallback(
    async (obj) => {
      try {
        const response = await axios.put(
          `https://${REACT_APP_CUSTOM_URL}/api/v1/User/SignIn`,
          obj
        );
        if (response.status == '200') {
          setAuth((prevState) => {
            return { ...prevState, ...response.data };
          });
          navigate('/Profile', { replace: true });
        } else {
          throw Error(response);
        }
      } catch (err) {
        console.log(err)
        alert(err.response.data.Title)
      }
    },
    [REACT_APP_CUSTOM_URL]
  );
  return { LoginHandler };
};

export default useLogin;
