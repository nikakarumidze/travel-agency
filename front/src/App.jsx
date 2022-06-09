import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Header from './components/header/Header';
import Main from './pages/Main/Main';
import './App.scss';

const App = () => {
  return (
    <BrowserRouter>
      <Header />
      <Routes>
        <Route path='/' element={<Main />}/>
      </Routes>
    </BrowserRouter>
  );
};

export default App;
