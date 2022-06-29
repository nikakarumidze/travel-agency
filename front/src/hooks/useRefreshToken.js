import React from 'react';

const useRefreshToken = () => {
  const token = 'asdasd';
  const refreshToken = 'sadasdas';

  const obj = { token, refreshToken };

  const { REACT_APP_CUSTOM_URL } = process.env;

  const refreshTokenRequest = () => {
    fetch(`https://${REACT_APP_CUSTOM_URL}/api/v1/User/Refresh`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(obj),
    }).then((response) => {
      if (!response.ok) {
        console.log(response, 'err');
      } else {
        console.log(response, 'success');
        return response.json();
      }
    })
    .then(res=> console.log(res, 'sg'))
    .catch(err=> console.log(err, 'errorirn'))
  };
};

export default useRefreshToken;
