import { useCallback } from 'react';

const useCities = (applyData) => {
  const {REACT_APP_CUSTOM_URL} = process.env;
  const getCities = useCallback(async () => {
    const response = await fetch(`https://${REACT_APP_CUSTOM_URL}/api/v1/City/GetAll`);
    const allCities = await response.json();
    return applyData(allCities);
  }, [REACT_APP_CUSTOM_URL, applyData]);

  return getCities;
};

export default useCities;
