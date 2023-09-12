import { useState } from "react";
import { Link } from "react-router-dom";

import NotificationIcon from "../assets/icons/notificaciones.svg";
import PerfilIcon from "../assets/icons/perfil.svg";
import PerfilImg from "../assets/icons/perfil.png";

const ProfileModal = () => {
  return (
    <div className="bg-white p-4 shadow-md absolute top-20 right-0 flex flex-col gap-2 rounded-md">
      <Link
        className="text-black hover:text-slate-600 transition-colors"
        to="/profile"
      >
        Mi Perfil
      </Link>
      <button
        className="text-black hover:text-slate-600 transition-colors"
        to="/logout"
      >
        Cerrar Sesión
      </button>
    </div>
  );
};

const NotificationModal = () => {
  return (
    <div className="bg-white p-4 shadow-md absolute top-20 right-0 flex gap-4 border-b-2">
      <div>
        <img
          width={30}
          height={30}
          src={PerfilImg}
          alt="Imagen de perfil del usuario"
        />
      </div>
      <p className="text-black">
        <strong>Lorena Casa</strong> ha comentado tu reporte
      </p>
    </div>
  );
};

export default function Navbar() {
  const [profileOptions, setProfileOptions] = useState(false);
  const [notificationesOptions, setNotificationOptions] = useState(false);

  return (
    <nav className="flex justify-between items-center w-full p-8">
      <Link to="/">
        <h3 className="text-primary-100 text-2xl font-bold">Red Co.</h3>
      </Link>
      <ul className="flex gap-4">
        <Link to="/muro">
          <li className="text-black hover:text-slate-600 transition-colors">
            Muro
          </li>
        </Link>
        <Link to="/report">
          <li className="text-black hover:text-slate-600 transition-colors">
            Nuevo Reporte
          </li>
        </Link>
        <div className="flex gap-4">
          <button>
            <img
              width="30"
              height="30"
              src={NotificationIcon}
              alt="icono de las notififaciones"
              onClick={() => setNotificationOptions(!notificationesOptions)}
            />
          </button>
          {notificationesOptions ? (
            <>
              <NotificationModal />
            </>
          ) : null}
          <button
            className="relative"
            onClick={() => setProfileOptions(!profileOptions)}
          >
            <img
              width="30"
              height="30"
              src={PerfilIcon}
              alt="icono del usuario"
            />
          </button>
          {profileOptions ? <ProfileModal /> : null}
        </div>
      </ul>
    </nav>
  );
}
