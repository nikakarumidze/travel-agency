import { createContext, useState } from 'react';

const AuthContext = createContext({});
const contextData = { token: null, refreshToken: null, userName: null };

export const AuthProvider = ({ children }) => {
  const [auth, setAuth] = useState(contextData);
  return (
    <AuthContext.Provider value={{ auth, setAuth }}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContext;
