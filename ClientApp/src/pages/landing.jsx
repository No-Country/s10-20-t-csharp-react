import React from "react";
import { Link } from "react-router-dom";

import { MainLayout } from "../layout/MainLayout";
import HelpIcon from "../assets/icons/help-information-question.svg";
import SpeakerIcon from "../assets/icons/broadcast.svg";
import PressIcon from "../assets/icons/press.svg";
import LikeIcon from "../assets/icons/like.svg";
import puzzle from "../assets/puzzle.png";
import flechaAbajo from "../assets/icons/flecha.svg";
import logo from "../assets/logo.svg";

const featuresItems = [
  <div id="first-row" className="flex gap-8 w-42">
    <div className="flex gap-2 items-center">
      <img width="80" height="80" src={HelpIcon} alt="Ícono sobre ayudar" />
      <p className="w-[28ch]">
        Ayudá a visibilizar las problematicas frecuentes en los bienes públicos
        de tu ciudad,
      </p>
    </div>

    <div className="flex gap-2 items-center">
      <img width="80" height="80" src={SpeakerIcon} alt="Ícono de un altavoz" />
      <p className="w-[28ch]">Entérate de los reportes de otros ciudadanos,</p>
    </div>
  </div>,
  <div id="second-row" className="flex gap-8">
    <div className="flex gap-2 items-center">
      <img
        width="80"
        height="80"
        src={PressIcon}
        alt="Ícono sobre una mano presionando"
      />
      <p className="w-[28ch]">
        Contribuí a mantener los espacios comunes en óptimas condiciones
      </p>
    </div>
    <div className="flex gap-2 items-center">
      <img
        width="80"
        height="80"
        src={LikeIcon}
        alt="Ícono de pulgares arriba"
      />
      <p className="w-[28ch]">Sé responsable...</p>
    </div>
  </div>,
];

export default function Landing() {
  return (
    <MainLayout>
      <main className="px-28">
        <div
          id="hero-section"
          className="h-screen flex flex-col items-center gap-4 justify-center"
        >
          <div className="flex justify-center items-center gap-4">
            <div id="hero-section__img">
              <img
                width="500"
                height="500"
                src={puzzle}
                alt="Personas sobre un rompecabezas"
                className=""
              />
            </div>
            <div
              id="hero-section__content"
              className="flex flex-col justify-center items-center gap-6"
            >
              <img className="w-96" src={logo} alt="Logo de la página" />
              <p className="w-[30ch] text-center text-xl text-black">
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
              className="mt-20"
              src={flechaAbajo}
              alt="Flecha apuntando hacia abajo"
              width="50"
              height="50"
            />
          </div>
        </div>

        <section className="flex justify-center items-center gap-8 py-16">
          <ul className="flex flex-col gap-10 items-center justify-center">
            {featuresItems.map((item, id) => (
              <li key={id} className="text-lg text-black w-[70ch]">
                {<p>{item}</p>}
              </li>
            ))}
          </ul>
        </section>
      </main>
    </MainLayout>
  );
}
