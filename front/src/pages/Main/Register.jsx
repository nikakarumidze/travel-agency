import { Box, Container, TextField, Typography } from '@mui/material';
import BoxModal from '../../components/UI/BoxModal';

import useForm from './hooks/useForm';

const Register = (props) => {
  const {
    formState,
    validityChangeHandler,
    firstNameChangeHandler,
    lastNameChangeHandler,
    emailChangeHandler,
    passwordChangeHandler,
  } = useForm();

  const registrationHandler = (e) => {
    e.preventDefault();
    console.log(formState);

    if (!formState.firstName || !formState.lastName) {
      if (!formState.firstName.trim()) {
        validityChangeHandler('isFirstNameValid', false);
      }
      if (!formState.lastName.trim()) {
        validityChangeHandler('isLastNameValid', false);
      }
      // is email valid
      // is password valid
    
      return;
    }
    // Validate Params
    // If everything is ok, send request
    // is not return error(s)
  };

  return (
    <Container align='center' sx={{mb:7}}>
      <Typography variant='h3' component='h1' align='center'>
        Register Now
      </Typography>
      <Typography variant='h6' component='p' align='center' sx={{ p: 3 }}>
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
          sx={{ mt: 1, mb: 3 }}
          error={!formState.isPasswordValid}
        />
      </BoxModal>
    </Container>
  );
};

export default Register;
