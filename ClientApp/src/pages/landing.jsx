import React from "react";
import { Link } from "react-router-dom";

import puzzle from "../assets/puzzle.png";
import flechaAbajo from "../assets/icons/flecha.svg";
import logo from "../assets/logo.svg";
import { MainLayout } from "../layout/MainLayout";

const featuresItems = [
  "Ayudá a visibilizar las problematicas frecuentes en los bienes publicos de tu ciudad",
  "Enterate de los reportes de otros ciudadanos",
  "Controbuí a mantener los espacios comunes en optimas condiciones",
  "Sé responsable...",
];

export default function Landing() {
  return (
    <MainLayout>
      <main className="px-28">
        <div
          id="hero-section"
          className="h-screen flex flex-col items-center gap-4 sm:gap-2 justify-center"
        >
          <div className="flex justify-center items-center gap-4">
            <div id="hero-section__img">
              <img
                width="500"
                height="500"
                src={puzzle}
                alt="Personas sobre un rompecabezas"
                className="sm:w-4"
              />
            </div>
            <div
              id="hero-section__content"
              className="flex flex-col justify-center items-center gap-6"
            >
              <img
                className="w-96 sm:w-56"
                src={logo}
                alt="Logo de la página"
              />
              <p className="w-[30ch] text-center text-xl sm:text-sm text-black">
                Tu aporte como ciudadano ayuda a visibilizar problemas en los
                bienes de uso públicos
              </p>
              <div className="flex gap-2">
                <Link
                  to="/register"
                  className="font-bold text-white border bg-terciary-100 p-2 rounded-2xl hover:bg-terciary-50 transition-colors"
                >
                  Registrarme
                </Link>
                <Link
                  to="/login"
                  className="border-terciary-100 font-bold text-terciary-100 border p-2 rounded-2xl hover:border-2 hover:border-terciary-50 transition-colors"
                >
                  Iniciar sesión
                </Link>
              </div>
            </div>
          </div>
          <div>
            <img
              className="mt-20 sm:w-12"
              src={flechaAbajo}
              alt="Flecha apuntando hacia abajo"
              width="50"
              height="50"
            />
          </div>
        </div>

        <section className="flex flex-col items-center gap-8 py-10">
          <ul className="flex flex-col gap-4">
            {featuresItems.map((item, id) => (
              <li
                key={id}
                className="text-title-lg sm:text-base sm:w-[25ch] sm:text-center text-black"
              >
                {item}
              </li>
            ))}
          </ul>
        </section>
      </main>
    </MainLayout>
  );
}
