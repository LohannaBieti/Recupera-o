import { useEffect, useState } from "react";
import { Aluno } from "../../modelos/Aluno";
import React from "react";

function CadastrarProduto(){

    const [nome, setNome] = useState("");
    const [sobrenome, setSobrenome] = useState("");

    function HandleSubmit(e:any){

        e.preventDefault()

        const aluno : Aluno = {
            nome: nome,
            sobrenome: sobrenome,
            alunoId: "",
            criadoEm: ""
        }

        fetch("http://localhost:5022/cadastrar/aluno" , {
            method: "POST",
            headers: {
                "Content-Type": "application/json",

            },
            body: JSON.stringify(aluno)
        })
        .then(resposta => {return resposta.json()})
        .then(produtoCriado => console.log(produtoCriado));
    }

    return (
        <div>
            <form onSubmit={HandleSubmit}>
                <div>
                    <label htmlFor="nome">Nome:</label>
                    <input type="text" onChange={(e:any) => setNome(e.target.value)}/>
                </div>
                <div>
                <label htmlFor="sobrenome">Sobrenome:</label>
                    <input type="text" onChange={(e:any) => setSobrenome(e.target.value)}/>
                </div>
                <button type="submit">Cadastrar</button>
            </form>
        </div>
    )
}

export default CadastrarAluno;