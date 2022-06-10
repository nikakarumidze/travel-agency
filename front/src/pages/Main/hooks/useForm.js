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

  const onTypingHandler = (event, actionType, validityType) => {
    formDispatch({ type: actionType, value: event.target.value });
    if (!formState[validityType]) {
      formDispatch({ type: validityType, value: true });
    }
  };

  return {
    formState,
    onTypingHandler,
    validityChangeHandler,
  };
};

export default useForm;