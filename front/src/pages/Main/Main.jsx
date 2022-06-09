import React, { useState } from 'react';
import Login from './Login';
import Register from './Register';

const Main = () => {
  const [onRegister, setOnRegister] = useState(false);
  return (
    <>
      {!onRegister && <Login onRegister={() => setOnRegister(true)} />}
      {onRegister && <Register onLogin={() => setOnRegister(false)} />}
    </>
  );
};

export default Main;
