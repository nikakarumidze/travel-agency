import { useCallback, useState } from 'react';

const useRefreshToken = () => {
  const [newToken, setNewToken] = useState();
  const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjA1ZmFjNWY5LTBlODQtNDA4ZS1iZjUzLTVjNTlmYWI4Nzg4OCIsImp0aSI6IjdhNzIyYzNjLWQ5NjQtNGM5Ni1iZTBiLWZhYmUyNTI0N2I4YiIsIm5hbWUiOiJuaWthMjEiLCJuYmYiOjE2NTY3NTYyNjQsImV4cCI6MTY1Njc1NjU2NCwiaWF0IjoxNjU2NzU2MjY0LCJpc3MiOiJsb2NhbGhvc3QiLCJhdWQiOiJsb2NhbGhvc3QifQ.fddSV3z-2xew6ZbHeKU2OdS97l3Sr9PWWUNUyJLohiw";
  const refreshToken = "72f31fef-d600-49f7-a94a-13f56db9c253" ;

  const obj = { token, refreshToken };

  const { REACT_APP_CUSTOM_URL } = process.env;

  const refreshTokenRequest = useCallback(async () => {
    try {
      const response = await fetch(
        `${REACT_APP_CUSTOM_URL}/api/v1/User/Refresh`,
        {
          method: 'PUT',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(obj),
        }
      );
      if (!response.ok) {
        console.log(response)
        throw Error(response.status);
      }
      const json = await response.json();
      console.log(json);
      setNewToken(json);
    } catch (err) {
      console.log(err);
    }
  }, [REACT_APP_CUSTOM_URL]);
  return { refreshTokenRequest, newToken };
};

export default useRefreshToken;
