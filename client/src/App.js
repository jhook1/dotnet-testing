import logo from './logo.svg';
import './App.css';
import { useState } from 'react';

function apiBuilder(path, state, data) {
  const apiUrl = "https://localhost:5001"

  function view() {
    fetch(`${apiUrl}${path}/${data.id}`)
      .then(async (res) => {
        state.setBuilder(await res.text());
      });
  }

  function list() {
    console.log("list");
    fetch(`${apiUrl}${path}`)
      .then(async (res) => {
        state.setBuilder(await res.text());
      });
  }

  function create() {
    fetch(`${apiUrl}${path}`, { method: "POST", body: data })
      .then(async (res) => {
        state.setBuilder(await res.text());
      });
  }

  return {
    view,
    list,
    create
  }
}

function App() {
  const [name, setName] = useState("");
  const [desc, setDesc] = useState("");
  const [dispResp, setDispResp] = useState("");
  const [builder,setBuilder] = useState("");

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
      .then(() => { setDesc(""); setName(""); })
      .catch((err)=>console.log(err));
  };

  const handleApiBuilder = (e) => {
    e.preventDefault();
    //const resp = 
    apiBuilder("/other/stuff/",{builder,setBuilder}).list();
    //console.log(resp);
    //setBuilder(resp);
  }

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
        <br/>
        <form onSubmit={(e)=>handleApiBuilder(e)}>
          <button>API Builder</button>
        </form>
        <span>Builder: {builder}</span>
      </header>
    </div>
  );
}

export default App;
