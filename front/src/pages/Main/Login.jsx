import BoxModal from '../../components/UI/BoxModal';
import TextField from '@mui/material/TextField';

import React, { useState } from 'react';

const Login = (props) => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [usernameIsValid, setUsersNameisValid] = useState(true);
  const [passwordisValid, setPasswordisValid] = useState(true);

  const usernameChangeHandler = (e) => {
    setUsersNameisValid(true);
    if (!usernameIsValid) {
      setUsername(e.target.value);
    }
  };

  const passwordChangeHandler = (e) => {
    setPassword(e.target.value);
    if (!passwordisValid) {
      setPasswordisValid(true);
    }
  };

  const loginHandler = (e) => {
    e.preventDefault();
    if (!username || !password) {
      if (!username.trim()) {
        setUsersNameisValid(false);
      }
      if (!password.trim()) {
        setPasswordisValid(false);
      }
      return;
    }

    console.log(username, password);
    // fetch .... check username and password and return token if true.
    // Are we checking the validity of username?
    // Should we add reset password?
  };

  return (
    <BoxModal
      title='Login'
      login='submit'
      register='button'
      onSubmit={loginHandler}
      onRegister={props.onRegister}
      alignTitle='center'
      regVariant='outlined'
      logVariant='contained'
    >
      <TextField
        required
        label='Login'
        variant='outlined'
        onChange={usernameChangeHandler}
        error={!usernameIsValid}
        fullWidth
        sx={{mb:2}}
      />
      <br />
      <TextField
        required
        label='Password'
        variant='outlined'
        onChange={passwordChangeHandler}
        fullWidth
        error={!passwordisValid}
      />
      <br />
    </BoxModal>
  );
};

export default Login;
