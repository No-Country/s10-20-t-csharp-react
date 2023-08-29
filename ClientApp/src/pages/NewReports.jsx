import React from 'react'
import myReportsImg from '../assets/img_myreports.png'

const NewReports = () => {
    return (
    <>
        <div className="flex w-full items-start justify-center mt-20">
            <div className="w-[70%] flex flex-col gap-4 items-center">
                <h1 className="text-3xl">La convivencia ciudadana y cuidar los espacios comunes es una responsabilidad colectiva.</h1>
            </div>
        </div>
        <div className="flex w-full items-start justify-center mt-20">
            <div className="w-[45%]">
                <p className="text-xl">Tu reporte ayuda a mantener estos espacios en optimas condiciones.</p>
            </div>
            <div className="w-[25%] flex flex-col gap-4 items-center">
                <button className=" bg-[#1E3756] w-[60%] p-5 rounded-full text-white text-xl">Nuevo Reporte</button>
                <button className="bg-[#1E375680] w-[60%] p-5 rounded-full text-white text-xl">Mis Reportes</button>
            </div>
        </div>
        <div className="flex w-[70%] items-start justify-center mt-1">
            <div className="w-[70%] flex flex-col gap-4 h-full">
                <img className="ms-20" src={myReportsImg} alt="Imagen" />
            </div>
        </div>
    </>
    
    )
}

export default NewReports