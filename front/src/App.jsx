import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom';
import Header from './components/header/Header';
import RequireAuth from './components/RequireAuth';
import Profile from './pages/Profile/Profile';
import Search from './pages/Search/Search';
import Login from './pages/Login/Login';
import Register from './pages/Register/Register';
import Missing from './pages/Missing/Missing';
import Guests from './pages/Guests/Guests';
import './App.scss';

const App = () => {
  return (
    <div className='main'>
      <BrowserRouter>
        <Header />
        <Routes>
          <Route path='/' element={<Navigate to='/Login' replace/>}/>
          
          <Route path='/Login' element={<Login />} />
          <Route path='/Register' element={<Register />} />

          <Route element={<RequireAuth />}>
            <Route path='/Profile' element={<Profile />} />
            <Route path='/Search' element={<Search />} />
          </Route>
          <Route path='/MyGuests' element={<Guests />}/>

          <Route path='*' element={<Missing />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
};

export default App;
