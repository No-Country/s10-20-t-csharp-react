import { useState } from "react";
import { NewReport } from "../components/NewReport";
import React from "react"
import axios from "axios"
import {useEffect} from "react"
import { useContext } from 'react';
import { UserContext } from '../store/userContext';

const Report = () => {

  const [isOpen, setIsOpen] = useState(false);
  const userCtx = useContext(UserContext)

  useEffect(() => { 
    axios.get("https://s10nc.somee.com/api/Auth/IsAuthenticated")
        .then((res) => { 
          console.log(res.data)
        })
        .catch((err) => { 
          console.log(err)
        })
  }, [])

  useEffect(() => { 
    axios.get("https://s10nc.somee.com/api/me")
        .then((res) => { 
          console.log(res.data)
          userCtx.updateUserName(res.data.name)
          userCtx.updateUserEmail(res.data.email)
          userCtx.updateUserProfileImage(res.data.picture_Url)
        })
        .catch((err) => { 
          console.log(err)
        })
  }, [])

  return (
    <>
      <div className="flex flex-col w-full items-start justify-center">
        <div className="flex gap-4 items-center w-full p-10">
          <div className="">
            <h2 className="text-xl font-medium">
              La convivencia ciudadana y cuidar los espacios comunes es una
              responsabilidad colectiva.
            </h2>

            <p className="font-light text-lg mt-12">
              El estado es el encargado de mantener estos espacios en optimas
              condiciones de funcionamiento, seguridad e higiene.{" "}
            </p>
            <h2 className="text-lg font-medium mt-4">
              ¿Qué pasa cuando esto no funciona como debería?
            </h2>
            <p className="font-light mt-3">
              Tu reporte como ciudadano colabora con la visibilización de estas
              situaciones que necesitan ser atendidas con urgencia.{" "}
            </p>
          </div>
          <div className="w-[50%] flex flex-col gap-4 items-center mt-12">
            <button
              className="bg-[#D9D9D9] w-full p-5 rounded-xl font-medium"
              onClick={() => setIsOpen(!isOpen)}
            >
              Nuevo Reporte
            </button>
            {isOpen ? (
              <div className="p-4">
                <NewReport isOpen={isOpen} setIsOpen={setIsOpen} />
              </div>
            ) : null}
            <button className="bg-[#D9D9D9] w-full pl-7 pr-7 pt-5 pb-5 rounded-xl font-medium ">
              Mis Reportes
            </button>
          </div>
        </div>

        <div className="m-8 w-full">
          <p className="text-start font-medium">Preguntas Frecuentes</p>

          <div className="w-[50%] h-20 bg-[#D9D9D9]"></div>
        </div>
      </div>
    </>
  );
};

export default Report;
