import './App.css';
import { Routes, Route } from 'react-router-dom';
import HomePage from './pages/HomePage';
import StoragePage from './pages/StoragePage';
import AddTransfer from './pages/AddTransfer';

function App() {
  return (
    <Routes>
      <Route exact path="/" element={ <HomePage /> }></Route>
      <Route path="/storages" element={ <StoragePage /> }></Route>
      <Route path="/add-transfer" element={ <AddTransfer /> }></Route>
    </Routes>
  );
}

export default App;
