import { useCallback, useState } from 'react';

const useRegister = () => {
  const [response, setResponse] = useState();
  const { REACT_APP_CUSTOM_URL } = process.env;

  const UserRegister = useCallback(
    async (obj) => {
      try {
        const res = await fetch(
          `${REACT_APP_CUSTOM_URL}/api/v1/User/Register`,
          {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(obj),
          }
        );
        if (!res.ok) {
          const json = await res.json();
          setResponse(json.Title);
          throw Error(json.Title);
        }
        setResponse('successfully registered');
      } catch (error) {
        console.log(error);
      }
    },
    [REACT_APP_CUSTOM_URL]
  );
  return { response, UserRegister };
};

export default useRegister;
