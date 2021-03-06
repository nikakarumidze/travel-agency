import BoxModal from '../../components/UI/BoxModal';
import TextField from '@mui/material/TextField';
import validator from 'validator';

import useForm from '../../hooks/useForm';
import useLogin from './hooks/useLogin';

const Login = (props) => {
  const { formState, onTypingHandler, validityChangeHandler } = useForm();
  const {LoginHandler} = useLogin();

  const loginHandler = (e) => {
    e.preventDefault();
    const userNameValidity = !validator.isEmpty(formState.userName);
    const passwordValidity = validator.isStrongPassword(formState.password, {
      minLength: 8,
      minLowercase: 1,
      minUppercase: 1,
      minNumbers: 1,
      minSymbols: 0,
    });

    if (!userNameValidity || !passwordValidity) {
      if (!userNameValidity) {
        validityChangeHandler('isUserNameValid', false);
      }
      if (!passwordValidity) {
        validityChangeHandler('isPasswordValid', false);
      }
      return;
    }

    const obj = { username: formState.userName, password: formState.password };
    LoginHandler(obj)
    .catch((err)=> {
      alert(err.data.Title);
      validityChangeHandler('isUserNameValid', false);
      validityChangeHandler('isPasswordValid', false);
    })
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
        onChange={(event) =>
          onTypingHandler(event, 'userName', 'isUserNameValid')
        }
        error={!formState.isUserNameValid}
        fullWidth
        sx={{ mb: 2 }}
        helperText={!formState.isUserNameValid ? 'Login is Invalid' : ''}
      />
      <TextField
        required
        label='Password'
        variant='outlined'
        type='password'
        onChange={(event) =>
          onTypingHandler(event, 'password', 'isPasswordValid')
        }
        fullWidth
        error={!formState.isPasswordValid}
        sx={{ mb: 3 }}
        helperText={!formState.isPasswordValid ? 'Password is Invalid' : ''}
      />
    </BoxModal>
  );
};

export default Login;
