import { Box, Button, Container, TextField, Typography } from '@mui/material';
import BoxModal from '../../components/UI/BoxModal';
import validator from 'validator';

import useForm from '../../hooks/useForm';

const Register = (props) => {
  const { formState, onTypingHandler, validityChangeHandler } = useForm();

  const registrationHandler = (e) => {
    e.preventDefault();
    const firstNameValidity = !validator.isEmpty(formState.firstName);
    const lastNameValidity = !validator.isEmpty(formState.lastName);
    const userNameValidity = !validator.isEmpty(formState.userName);
    const emailValidity = validator.isEmail(formState.email);
    const passwordValidity = validator.isStrongPassword(formState.password, {
      minLength: 8, minLowercase: 1,
      minUppercase: 1, minNumbers: 1, minSymbols: 0
    });

    if (!firstNameValidity || !lastNameValidity || !userNameValidity || !emailValidity || !passwordValidity) {
      if (!firstNameValidity) {
        validityChangeHandler('isFirstNameValid', false);
      }
      if (!lastNameValidity) {
        validityChangeHandler('isLastNameValid', false);
      }
      if (!userNameValidity) {
        validityChangeHandler('isUserNameValid', false);
      }
      if (!emailValidity) {
        validityChangeHandler('isEmailValid', false);
      }
      if (!passwordValidity) {
        validityChangeHandler('isPasswordValid', false);
      }
      return;
    }
    console.log(formState);
    // Validate Params
    // If everything is ok, send request
    // is not return error(s)
  };

  return (
    <Container align='center' sx={{ mb: 7 }}>
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
            onChange={(event) =>
              onTypingHandler(event, 'firstName', 'isFirstNameValid')
            }
            sx={{ my: 1 }}
            error={!formState.isFirstNameValid}
          />
          <TextField
            required
            label='Last Name'
            variant='outlined'
            onChange={(event) =>
              onTypingHandler(event, 'lastName', 'isLastNameValid')
            }
            sx={{ my: 1 }}
            error={!formState.isLastNameValid}
          />
        </Box>
        <TextField
          required
          fullWidth
          label='Username'
          variant='outlined'
          onChange={(event) =>
            onTypingHandler(event, 'userName', 'isUserNameValid')
          }
          sx={{ my: 1 }}
          error={!formState.isUserNameValid}
        />
        <br />
        <TextField
          required
          fullWidth
          label='Email address'
          variant='outlined'
          type='email'
          onChange={(event) => onTypingHandler(event, 'email', 'isEmailValid')}
          sx={{ my: 1 }}
          error={!formState.isEmailValid}
          helperText={
            !formState.isEmailValid
              ? 'Please provide a valid Email'
              : ''
          }
        />
        <br />
        <TextField
          label='Create password'
          variant='outlined'
          fullWidth
          required
          type='password'
          onChange={(event) =>
            onTypingHandler(event, 'password', 'isPasswordValid')
          }
          helperText={
            !formState.isPasswordValid
              ? 'Password must contain more than 8 characters, At least 1 LowerCase, 1 Uppercase letters and 1 Number'
              : ''
          }
          sx={{ mt: 1, mb: 3 }}
          error={!formState.isPasswordValid}
        />
        <Button variant='contained' color='secondary' component='label' sx={{mb:4}}>
          Profile image
          <input type='file' hidden accept='image/png, image/jpeg' />
        </Button>
      </BoxModal>
    </Container>
  );
};

export default Register;
