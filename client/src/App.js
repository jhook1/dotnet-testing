import logo from './logo.svg';
import './App.css';
import { useState } from 'react';

function App() {
  const [name, setName] = useState("");
  const [desc, setDesc] = useState("");
  const [dispResp, setDispResp] = useState("");

  const handleSubmitAction = (e) => {
    e.preventDefault();
    const res = {
      "Id": Math.floor(Math.random() * 10),
      "Name": name,
      "Description": desc
    }
    const payload = {
      method: "POST",
      headers: { "content-type": "application/json" },
      body: JSON.stringify(res)
    }
    fetch("https://localhost:5001/stuff", payload)
      .then(async (res) => setDispResp(await res.text()))
      .catch((err)=>console.log(err));
  };

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <form onSubmit={ handleSubmitAction }>
          <input type="text" value={name} onChange={(e) => setName(e.target.value)}></input>
          <input type="text" value={desc} onChange={(e) => setDesc(e.target.value)}></input>
          <button>Submit</button>
        </form>
        <span>Response: { dispResp }</span>
      </header>
    </div>
  );
}

export default App;
