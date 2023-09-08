import { Link } from "react-router-dom";
import { useState } from "react";

import NotificationIcon from "../assets/icons/notificaciones.svg";
import PerfilIcon from "../assets/icons/perfil.svg";

import Notifications from "./Notifications";

export default function Navbar() {
  const [isOpen, setIsOpen] = useState(false)

  return (
    <nav className="flex justify-between items-center w-full p-8">
      <Link to="/">
        <h3 className="text-primary-100">Red Co.</h3>
      </Link>
      <ul className="flex gap-4">
        <Link to="/muro">
          <li>Muro</li>
        </Link>
        <Link to="#">
          <li>Nuevo Reporte</li>
        </Link>
        <div className="flex gap-4">
          <Link onClick={() => setIsOpen(!isOpen)}>
            <img
              width="30"
              height="30"
              src={NotificationIcon}
              alt="icono de las notififaciones"
            />
          </Link>
          <div className={`${isOpen ? 'hidden' : 'flex'} absolute top-0`}>
            <Notifications />
          </div>
          <Link>
            <img
              width="30"
              height="30"
              src={PerfilIcon}
              alt="icono del usuario"
            />
          </Link>
        </div>
      </ul>
    </nav>
  );
}
