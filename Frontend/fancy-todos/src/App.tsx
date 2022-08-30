import React, { useEffect, useState } from 'react';
import logo from './logo.svg';
import './App.css';
import axios from 'axios';

function App() {
  const [todos, setTodos] = useState([]);

  useEffect(() => {
    axios.get('https://localhost:5001/api/todos').then((response) => {
      console.log(response);
      console.log(todos);
      setTodos(response.data);
    });
  }, []);

  return (
    <div className='App'>
      <header className='App-header'>
        <ul>
          {todos.map((todo: any) => {
            return <li key={todo.id}>{todo.title}</li>;
          })}
        </ul>
      </header>
    </div>
  );
}

export default App;
