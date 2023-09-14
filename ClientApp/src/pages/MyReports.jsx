import React from "react";
import myReportsImg from "../assets/img_myreports.png";
import Modal from "../components/Modal";
import { useState } from "react";
import { MainLayout } from "../layout/MainLayout";

const MyReports = () => {
  const [isOpen, setIsOpen] = useState(false);

  return (
    <>
      <MainLayout>
        <section className="py-28 relative">
          <div className="flex w-full items-start justify-center mt-4">
            <div className="w-96 flex flex-col gap-4 h-full">
              <img src={myReportsImg} alt="Imagen" />
            </div>
            <div className="flex flex-col gap-4 items-center text-center">
              <h1 className="text-3xl text-black font-semibold w-[23ch]">
                La convivencia ciudadana y cuidar los espacios comunes es una
                responsabilidad colectiva
              </h1>
              <p className="text-xl text-black w-[30ch]">
                Tu reporte ayuda a mantener estos espacios en optimas
                condiciones
              </p>
              <div className="flex gap-4 items-center">
                <button
                  className=" bg-[#1E375680] w-48 p-4 rounded-full text-white text-xl"
                  onClick={() => setIsOpen(!isOpen)}
                >
                  Nuevo Reporte
                </button>
                <button className="bg-[#1E3756] w-48 p-4 rounded-full text-white text-xl">
                  Mis Reportes
                </button>
              </div>
            </div>
          </div>

          {isOpen ? <Modal /> : null}
        </section>
      </MainLayout>
    </>
  );
};

export default MyReports;
