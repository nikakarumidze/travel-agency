import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Header from './components/header/Header';
import Main from './pages/Main/Main';
import './App.scss';
import Profile from './pages/Profile/Profile';
import Search from './pages/Search/Search';

const App = () => {
  return (
    <BrowserRouter>
      <Header />
      <Routes>
        <Route path='/' element={<Main />} />
        <Route path='/Profile' element={<Profile />} />
        <Route path='/Search' element={<Search />} />
      </Routes>
    </BrowserRouter>
  );
};

export default App;
