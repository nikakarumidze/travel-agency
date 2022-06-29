import { useState } from 'react';

const useProfileInfo = () => {
  const { REACT_APP_CUSTOM_URL } = process.env;
  const [data, setData] = useState();
  const token =
    'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjA1ZmFjNWY5LTBlODQtNDA4ZS1iZjUzLTVjNTlmYWI4Nzg4OCIsImp0aSI6IjNmMTdiNGQ1LTFjNzEtNDJhNy04MDA5LThjM2U0ZmVhNzA1ZSIsIm5iZiI6MTY1NjM5NDA1MSwiZXhwIjoxNjU2Mzk0MzUxLCJpYXQiOjE2NTYzOTQwNTEsImlzcyI6ImxvY2FsaG9zdCIsImF1ZCI6ImxvY2FsaG9zdCJ9.5DKWgUWOoS9KVWSh5P-JvtMspAewYqMalwuEDpOJYIE';
  const tokenRequest = `Bearer ${token}`;

  fetch(`https://${REACT_APP_CUSTOM_URL}/api/v1/User/GetInfo`, {
    headers: {
      Authorization: tokenRequest,
      'Content-Type': 'application/json',
    },
  })
    .then((res) => {
      if (!res.ok) {
        console.log(res, 'erroria');
      } else {
        console.log(res, 'sg');
        return res.json();
      }
    })
    .then((res) => console.log(res))
    .catch((err) => console.log(err));

  fetch(`https://${REACT_APP_CUSTOM_URL}/api/v1/Apartment/GetMine`, {
    headers: {
      Authorization: tokenRequest,
      'Content-Type': 'application/json',
    },
  })
    .then((res) => res.json())
    .then((res) => console.log(res))
    .catch((err) => console.log(err));
};

export default useProfileInfo;
