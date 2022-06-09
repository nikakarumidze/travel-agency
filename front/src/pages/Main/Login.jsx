import BoxModal from '../../components/UI/BoxModal';
import TextField from '@mui/material/TextField';

import useForm from './hooks/useForm';

const Login = (props) => {
  const {
    formState,
    firstNameChangeHandler,
    passwordChangeHandler,
    validityChangeHandler,
  } = useForm();

  const loginHandler = (e) => {
    e.preventDefault();
    if (!formState.firstName || !formState.password) {
      if (!formState.firstName.trim()) {
        validityChangeHandler('isFirstNameValid', false);
      }
      if (!formState.password.trim()) {
        validityChangeHandler('isPasswordValid', false);
      }
      return;
    }
    console.log(formState);

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
        onChange={firstNameChangeHandler}
        error={!formState.isFirstNameValid}
        fullWidth
        sx={{ mb: 2 }}
      />
      <TextField
        required
        label='Password'
        variant='outlined'
        type='password'
        onChange={passwordChangeHandler}
        fullWidth
        error={!formState.isPasswordValid}
        sx={{mb: 3}}
      />
    </BoxModal>
  );
};

export default Login;
