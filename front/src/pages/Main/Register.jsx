import { Box, Container, TextField, Typography } from '@mui/material';
import React, { useReducer } from 'react';
import BoxModal from '../../components/UI/BoxModal';

const initialFormValue = {
  firstName: '',
  lastName: '',
  email: '',
  password: '',
  isFirstNameValid: true,
  isLastNameValid: true,
  isEmailValid: true,
  isPasswordValid: true,
};

const formReducer = (state, action) => {
  switch (action.type) {
    case 'firstName':
      return { ...state, firstName: action.value };
    case 'lastName':
      return { ...state, lastName: action.value };
    case 'email':
      return { ...state, email: action.value };
    case 'password':
      return { ...state, password: action.value };
    case 'isFirstNameValid':
      return { ...state, isFirstNameValid: action.value };
    case 'islastNameValid':
      return { ...state, isLastNameValid: action.value };
    case 'isEmailValid':
      return { ...state, isEmailValid: action.value };
    case 'isPasswordValid':
      return { ...state, isPasswordValid: action.value };
    default:
      return state;
  }
};

const Register = (props) => {
  const [formState, formDispatch] = useReducer(formReducer, initialFormValue);

  const firstNameChangeHandler = (event) => {
    formDispatch({ type: 'firstName', value: event.target.value });
    if (!formState.isFirstNameValid) {
      formDispatch({ type: 'isFirstNameValid', value: true });
    }
  };

  const lastNameChangeHandler = (event) => {
    formDispatch({ type: 'lastName', value: event.target.value });
    if (!formState.isLastNameValid) {
      formDispatch({ type: 'isLastNameValid', value: true });
    }
  };

  const emailChangeHandler = (event) => {
    formDispatch({ type: 'email', value: event.target.value });
    if (!formState.isEmailValid) {
      formDispatch({ type: 'isEmailValid', value: true });
    }
  };

  const passwordChangeHandler = (event) => {
    formDispatch({ type: 'password', value: event.target.value });
    if (!formState.isPasswordValid) {
      formDispatch({ type: 'isPasswordValid', value: true });
    }
  };

  const registrationHandler = (e) => {
    e.preventDefault();
    console.log(formState);
    // Validate Params
    // If everything is ok, send request
    // is not return error(s)
  };

  return (
    <Container align='center'>
      <Typography variant='h3' component='h1' align='center'>
        Register Now
      </Typography>
      <Typography variant='h5' component='p' align='center' sx={{ p: 3 }}>
        Register and gain the chance to travel for free with our culpa quas!
        Aspernatur quasi odio et officiis!
      </Typography>
      <BoxModal
        title='Sign Up'
        alignTitle='left'
        onSubmit={registrationHandler}
        onLogin={props.onLogin}
        login='button'
        register='submit'
        regVariant='contained'
        logVariant='outlined'
      >
        <Box sx={{ display: 'flex', justifyContent: 'space-between' }}>
          <TextField
            required
            label='First Name'
            variant='outlined'
            onChange={firstNameChangeHandler}
            sx={{ my: 1 }}
            error={!formState.isFirstNameValid}
          />
          <TextField
            required
            label='Last Name'
            variant='outlined'
            onChange={lastNameChangeHandler}
            sx={{ my: 1 }}
            error={!formState.isLastNameValid}
          />
        </Box>
        <TextField
          required
          fullWidth
          label='Email address'
          variant='outlined'
          type='email'
          onChange={emailChangeHandler}
          sx={{ my: 1 }}
        />
        <br />
        <TextField
          label='Create password'
          variant='outlined'
          fullWidth
          required
          type='password'
          onChange={passwordChangeHandler}
          sx={{ my: 1 }}
          error={!formState.isPasswordValid}
        />
        <br />
      </BoxModal>
    </Container>
  );
};

export default Register;
