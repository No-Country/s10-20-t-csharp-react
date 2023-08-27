import React from "react";

import puzzle from "../../public/assets/puzzle.png";
import logo from "../../public/assets/logo.svg";
import { Link } from "react-router-dom";

export default function Landing() {
  return (
    <main className="h-screen p-2 flex justify-center items-center gap-4">
      <div>
        <img src={puzzle} alt="Personas sobre un rompecabezas" />
      </div>
      <div
        id="content"
        className="flex flex-col justify-center items-center gap-4"
      >
        <img className="w-96" src={logo} alt="Logo de la página" />
        <p className="w-[30ch] text-center text-lg">
          Tu aporte como ciudadano ayuda a visibilizar problemas en los bienes
          de uso públicos
        </p>
        <Link
          to="/register"
          className="font-bold text-primary-100 border border-primary-100 p-2 rounded-2xl hover:bg-primary-100 hover:text-white transition-colors"
        >
          ¡Hazte oír!
        </Link>
      </div>
    </main>
  );
}
