import { Link } from "react-router-dom";

import NotificationIcon from "../assets/icons/notificaciones.svg";
import PerfilIcon from "../assets/icons/perfil.svg";

export default function Navbar() {
  return (
    <nav className="flex justify-between items-center w-full p-8">
      <Link to="/">
        <h3 className="text-primary-100">Red Co.</h3>
      </Link>
      <ul className="flex gap-4">
        <Link to="#">
          <li>Muro</li>
        </Link>
        <Link to="#">
          <li>Nuevo Reporte</li>
        </Link>
        <div className="flex gap-4">
          <Link>
            <img
              width="30"
              height="30"
              src={NotificationIcon}
              alt="icono de las notififaciones"
            />
          </Link>
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
