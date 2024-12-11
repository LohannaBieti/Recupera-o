import React from "react";
import CadastrarProduto from "./components/pages/CadastrarAluno";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Link } from "react-router-dom";
import CadastrarAluno from "./components/pages/CadastrarAluno";


function App() {
  return (
    <div id="app">
      <BrowserRouter>
        <nav>
          <ul>
            <li>
              <Link to="/aluno/cadastrar">Cadastrar Aluno</Link>
            </li>
            <li>
            </li>
          </ul>
        </nav>
        <Routes>
          <Route path="/aluno/cadastrar" element={<CadastrarAluno/>}/>
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;

