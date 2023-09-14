import React from 'react'
import myReportsImg from '../assets/img_myreports.png'
import Modal from '../components/Modal'
import { useState } from 'react'
import { MainLayout } from '../layout/MainLayout'

const MyReports = () => {
    const [isOpen, setIsOpen] = useState(false)

    return (
    <>
        <MainLayout>
        <div className="flex w-full items-start justify-center mt-4">
            <div className="w-[70%] flex flex-col gap-4 items-center">
                <h1 className="text-3xl text-black font-medium">La convivencia ciudadana y cuidar los espacios comunes es una responsabilidad colectiva.</h1>
            </div>
        </div>
        <div className="flex w-full items-start justify-center mt-6">
            <div className="w-[45%]">
                <p className="text-xl text-black">Tu reporte ayuda a mantener estos espacios en optimas condiciones.</p>
            </div>
            <div className="w-[25%] flex flex-col gap-4 items-center">
                <button className=" bg-[#1E375680] w-[60%] p-5 rounded-full text-white text-xl hover:bg-[#1E3756]" onClick={() => setIsOpen(!isOpen) }>Nuevo Reporte</button>
                {/*<button className="bg-[#1E3756] w-[60%] p-5 rounded-full text-white text-xl">Mis Reportes</button>*/}
            </div>
        </div>
        <div className="flex w-[50%] ml-32 -mt-10 items-start justify-center">
            <div className="w-full flex flex-col gap-4 h-full">
                <img className="ms-20" src={myReportsImg} alt="Imagen" />
            </div>
        </div>
        {
            isOpen ? <Modal/> : null
        }
        </MainLayout>
    </>
    )
}

export default MyReports
