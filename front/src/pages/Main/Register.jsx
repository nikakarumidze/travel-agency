import { Container, TextField, Typography } from '@mui/material';
import React from 'react';
import BoxModal from '../../components/UI/BoxModal';

const Register = (props) => {
  const textFieldInputs = [
    'First Name',
    'Last Name',
    'Email address',
    'Create password',
  ];
  const registerHandler = () => {
    console.log('zd');
  };
  return (
    <Container align='center'>
      <Typography variant='h3' component='h1' align='center'>Register Now</Typography>
      <Typography variant='h5' component='p' align='center' sx={{p: 3}}>Lorem ipsum dolor sit amet c officiis temporibus culpa quas! Aspernatur quasi odio et officiis!</Typography>
      <BoxModal
        title='Sign Up'
        alignTitle={'left'}
        onRegister={registerHandler}
        onLogin={props.onLogin}
      >
        <TextField required label='First Name' variant='outlined' />
        <TextField required label='Last Name' variant='outlined' />
        <TextField
          required
          fullWidth
          label='Email address'
          variant='outlined'
        />
        <br />
        <TextField
          label='Create password'
          variant='outlined'
          fullWidth
          required
          type='password'
        />
        <br />
      </BoxModal>
    </Container>
  );
};

export default Register;
