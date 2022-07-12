import axios from 'axios';
import { useCallback, useState } from 'react';
import useAuth from '../../../hooks/useAuth';

const useProfileInfo = () => {
  const { REACT_APP_CUSTOM_URL } = process.env;
  const [data, setData] = useState();
  const { auth } = useAuth();

  const tokenRequest = `Bearer ${auth.token}`;

  const getInfo = useCallback(async () => {
    try {
      const response = await axios.get(
        `https://${REACT_APP_CUSTOM_URL}/api/v1/User/GetInfo`,
        {
          headers: {
            'Authorization': tokenRequest
          },
        }
      );
      if (response.status == "200") {
        return response.data;
      } else {
        console.log(response.status, 'sg tkven');
      }
    } catch (err) {
      console.log(err, 'shecdoma');
    }
  }, [REACT_APP_CUSTOM_URL]);

  // axios.put(`https://${REACT_APP_CUSTOM_URL}/api/v1/User/GetInfo`, {
  //       headers: {
  //         Authorization: auth.token,
  //         'Content-Type': 'application/json',
  //       },
  //     })
  //     .then((res) => {
  //       if (res.ok) {
  //         return res.json();
  //       } else {
  //         console.log('zd');
  //       }
  //     })
  //     .then((res) => {
  //       console.log(res);
  //       setData(res);
  //     })
  //     .catch((err) => console.log(err)),

  // fetch(`${REACT_APP_CUSTOM_URL}/api/v1/User/GetInfo`, {
  //   headers: {
  //     'Authorization': tokenRequest,
  //     'Content-Type': 'application/json',
  //   },
  // })
  //   .then((res) => {
  //     if (!res.ok) {
  //       console.log(res, 'erroria');
  //     } else {
  //       console.log(res, 'sg');
  //       return res.json();
  //     }
  //   })
  //   .then((res) => console.log(res))
  //   .catch((err) => console.log(err));

  // fetch(`https://${REACT_APP_CUSTOM_URL}/api/v1/Apartment/GetMine`, {
  //   headers: {
  //     'Authorization': tokenRequest,
  //     'Content-Type': 'application/json',
  //   },
  // })
  //   .then((res) => res.json())
  //   .then((res) => console.log(res))
  //   .catch((err) => console.log(err));
  return { data, getInfo };
};

export default useProfileInfo;
