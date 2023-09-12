import React from "react";
import SearchIcon from "@mui/icons-material/Search";
import axios from "axios";
import { useState } from "react";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

const FiltrosMuro = () => {
  const [searchParam, setSearchParam] = useState("");
  const [searchResults, setSearchResults] = useState([]);
  const navigate = useNavigate();

  /*  const searchParamPublication = () => { 
          axios.get(`http://localhost:4000/getPublicationsWithParams/${searchParam}`)
              .then((res) => { 
                console.log(res.data)
                setSearchResults(res.data)
                setTimeout(() => { 
                  navigate(`/publicationsSearched/${searchParam}`)
                },200)
                setTimeout(() => { 
                 window.location.reload()
                }, 500)
              })
              .catch((err) => { 
                console.log(err)
              })
        }*/

  return (
    <div>
      <div className="mr-4">
        <div className="flex relative">
          <input
            type="text"
            placeholder="Search"
            className="bg-slate-100 border border-slate-700 rounded-lg p-2 "
            onChange={e => setSearchParam(e.target.value)}
          />
          <div>
            <SearchIcon
              style={{
                cursor: "pointer",
                marginLeft: "3px",
                position: "absolute",
                top: "10px",
                right: "10px",
                color: "rgb(51 65 85)",
              }}
            />
          </div>
        </div>

        <div className="mt-6">
          <div className="flex mt-4">
            <input
              type="checkbox"
              className="checkbox checkbox-sm border-slate-700"
            />
            <p className="ml-2 text-black">Aceras</p>
          </div>

          <div className="flex mt-4">
            <input
              type="checkbox"
              className="checkbox checkbox-sm border-slate-700"
            />
            <p className="ml-2 text-black">Luminarias</p>
          </div>

          <div className="flex mt-4">
            <input
              type="checkbox"
              className="checkbox checkbox-sm border-slate-700"
            />
            <p className="ml-2 text-black">Limpieza</p>
          </div>

          <div className="flex mt-4">
            <input
              type="checkbox"
              className="checkbox checkbox-sm border-slate-700"
            />
            <p className="ml-2 text-black">Parques y Plazas</p>
          </div>

          <div className="flex mt-4">
            <input
              type="checkbox"
              className="checkbox checkbox-sm border-slate-700"
            />
            <p className="ml-2 text-black">Transito</p>
          </div>

          <div className="flex mt-4">
            <input
              type="checkbox"
              className="checkbox checkbox-sm border-slate-700"
            />
            <p className="ml-2 text-black">Todo</p>
          </div>

          <div className="flex mt-4">
            <button className="text-black">Eliminar todos los filtros</button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default FiltrosMuro;
