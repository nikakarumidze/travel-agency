import React, { useState } from 'react';
import Login from './Login';
import Register from './Register';

import classes from './Main.module.scss'

const Main = () => {
  const [onRegister, setOnRegister] = useState(false);
  return (
    <div className={classes.main}>
      {!onRegister && <Login onRegister={() => setOnRegister(true)} />}
      {onRegister && <Register onLogin={() => setOnRegister(false)} />}
    </div>
  );
};

export default Main;
