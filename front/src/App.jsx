import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Header from './components/header/Header';
import './App.scss';
import Profile from './pages/Profile/Profile';
import Search from './pages/Search/Search';
import Login from './pages/Login/Login';
import Register from './pages/Register/Register';

const App = () => {
  return (
    <div className='main'>
      <BrowserRouter>
        <Header />
        <Routes>
          <Route path='/Login' element={<Login />} />
          <Route path='/Register' element={<Register />} />
          <Route path='/Profile' element={<Profile />} />
          <Route path='/Search' element={<Search />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
};

export default App;
