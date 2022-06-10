import { useReducer } from 'react';
const initialFormValue = {
  firstName: '',
  lastName: '',
  userName: '',
  email: '',
  password: '',
  isFirstNameValid: true,
  isLastNameValid: true,
  isUserNameValid: true,
  isEmailValid: true,
  isPasswordValid: true,
};

const formReducer = (state, action) => {
  switch (action.type) {
    case 'firstName':
      return { ...state, firstName: action.value };
    case 'lastName':
      return { ...state, lastName: action.value };
    case 'userName':
      return { ...state, userName: action.value };
    case 'email':
      return { ...state, email: action.value };
    case 'password':
      return { ...state, password: action.value };
    case 'isFirstNameValid':
      return { ...state, isFirstNameValid: action.value };
    case 'isLastNameValid':
      return { ...state, isLastNameValid: action.value };
    case 'isUserNameValid':
      return { ...state, isUserNameValid: action.value };
    case 'isEmailValid':
      return { ...state, isEmailValid: action.value };
    case 'isPasswordValid':
      return { ...state, isPasswordValid: action.value };
    default:
      return state;
  }
};

const useForm = () => {
  const [formState, formDispatch] = useReducer(formReducer, initialFormValue);

  const validityChangeHandler = (type, value) => {
    formDispatch({ type: type, value: value });
  };

  const onTypingHandler = (event, type, validityType) => {
    console.log(formState);
    formDispatch({ type: type, value: event.target.value });
    if (!formState.validityType) {
      formDispatch({ type: validityType, value: true });
    }
  };

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

  const userNameChangeHandler = (event) => {
    formDispatch({ type: 'userName', value: event.target.value });
    if (!formState.isFirstNameValid) {
      formDispatch({ type: 'isUserNameValid', value: true });
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

  return {
    formState,
    onTypingHandler,
    validityChangeHandler,
    firstNameChangeHandler,
    lastNameChangeHandler,
    userNameChangeHandler,
    emailChangeHandler,
    passwordChangeHandler,
  };
};

export default useForm;
