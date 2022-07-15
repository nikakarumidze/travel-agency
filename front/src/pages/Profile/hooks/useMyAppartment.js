import { useState, useCallback } from 'react';
import useAuth from '../../../hooks/useAuth';
import axios from 'axios';

const useMyAppartment = () => {
  const { REACT_APP_CUSTOM_URL } = process.env;
  // Appartment is created or not
  const [isCreated, setIsCreated] = useState(false);
  const { auth } = useAuth();
  const tokenRequest = `Bearer ${auth.token}`;

  const getMyAppartment = useCallback(async () => {
    try {
      const response = await axios.get(
        `https://${REACT_APP_CUSTOM_URL}/api/v1/Apartment/GetMine`,
        {
          headers: {
            'Authorization': tokenRequest,
          },
        }
      );
      if (response.status == '200') {
        setIsCreated(true);
        return response.data;
      } else {
        console.log(response.status, 'no apartments found');
        throw Error('no apartments found');
      }
    } catch (err) {
      console.log(err, 'no apartments found');
    }
  }, [REACT_APP_CUSTOM_URL]);

  const changeApartmentInfo = useCallback(
    async (apartmentData) => {
      console.log(isCreated);
      if (isCreated) {
        try {
          const response = await axios.put(
            `https://${REACT_APP_CUSTOM_URL}/api/v1/Apartment/UpdateMine`,
            apartmentData,
            {
              headers: {
                'Authorization': tokenRequest,
              },
            }
          );
          if (response.status == '200') {
            return response;
          } else {
            console.log(response, 'could not handle request');
            throw Error(response);
          }
        } catch (err) {
          console.log(err, 'no apartments found');
        }
        return;
      }
      try {
        const response = await axios.post(
          `https://${REACT_APP_CUSTOM_URL}/api/v1/Apartment/CreateMine`,
          apartmentData,
          {
            headers: {
              'Authorization': tokenRequest,
            },
          }
        );
        console.log(apartmentData);
        if (response.status == '200') {
          setIsCreated(true);
          return response;
        } else {
          console.log(response.status, 'could not handle request');
          throw Error(response.data.Title);
        }
      } catch (err) {
        console.log(err);
      }
    },
    [REACT_APP_CUSTOM_URL]
  );
  return { getMyAppartment, changeApartmentInfo };
};

export default useMyAppartment;
