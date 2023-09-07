import { useState } from "react";
import { Link } from "react-router-dom";

import { MainLayout } from "../layout/MainLayout";
import { LikeIcon } from "../components/atoms/corazon";
import { EditProfile } from "../components/EditProfile";

import FotoCalle from "../assets/subir.png";
import ConfigIcon from "../assets/icons/configuracion.svg";
import ProfileImg from "../assets/icons/profile_img.png";
import LocationIcon from "../assets/icons/ubicacion2.svg";
import ComentarioIcon from "../assets/icons/comentario.svg";
import CompartirIcon from "../assets/icons/compartir.svg";

const ReportCard = () => {
  const [like, setLike] = useState(false);
  const [share, setShare] = useState(false);
  const [comment, setComment] = useState(false);

  return (
    <div className="p-4 flex flex-col justify-center gap-4 border-b-2">
      <div className="flex gap-8 justify-between">
        <div className="flex gap-2">
          <img
            className="w-8 h-8 rounded-2xl"
            width="20"
            height="20"
            src={ProfileImg}
            alt="Foto de perfil"
          />
          <div className="flex flex-col gap-4">
            <header className="flex flex-col gap-2">
              <small>Nombre de usuario</small>
              <h3>
                <strong>Titulo del reporte</strong>
              </h3>
              <p className="font-light">Descripción del repoorte</p>
            </header>
            <div className="flex flex-col gap-4">
              <div className="flex gap-2 items-center">
                <img
                  className="rounded-2xl"
                  width="30"
                  height="30"
                  src={LocationIcon}
                  alt="Icono de localización en un mapa"
                />
                <p className="text-slate-700">Nro 7 1555, La Plata</p>
              </div>
              <a href="#" className="text-terciary-100">
                Ver en mapa
              </a>
            </div>
            <div className="flex gap-2">
              <img src={FotoCalle} alt="Foto del usuario" />
              <img src={FotoCalle} alt="Foto del usuario" />
            </div>
            <ul className="flex gap-8 justify-between">
              <li>
                <button onClick={() => setLike(!like)} className="flex gap-2">
                  1 <LikeIcon fill={`${like ? "#df2b2b" : "#111"}`} />
                </button>
              </li>
              <li>
                <button className="flex gap-2">
                  1
                  <img
                    width="30"
                    height="30"
                    src={ComentarioIcon}
                    alt="Icono para comentario"
                  />
                </button>
              </li>
              <li>
                <button>
                  {" "}
                  <img
                    width="30"
                    height="30"
                    src={CompartirIcon}
                    alt="Icono para compartir"
                  />
                </button>
              </li>
            </ul>
          </div>
        </div>
        <div className="flex flex-col gap-2">
          <Link>Ir a mis reportes</Link>
          <p className="p-2 border text-primary-100 text-center">Acera</p>
        </div>
      </div>
    </div>
  );
};

export function MyProfile() {
  const [profileConfigModal, setProfileConfigModal] = useState(false);

  const handleClick = e => {};

  return (
    <div>
      <MainLayout>
        <section className="py-12">
          <div className="w-[40em] mx-auto border border-slate-400 shadow-sm p-4 flex flex-col gap-8 font-normal rounded-2xl">
            <div className="flex gap-8 items-center ">
              <div className="relative flex justify-center items-center">
                <img
                  className="rounded-full"
                  width="80"
                  height="80"
                  src={ProfileImg}
                  alt="Foto de perfil"
                />
              </div>
              <div className="flex flex-col gap-4">
                <header>
                  <h3>
                    <strong>Nombre</strong>
                  </h3>
                  <p className="font-light">Descripción personal</p>
                </header>
              </div>
              <button
                onClick={() => setProfileConfigModal(!profileConfigModal)}
              >
                {profileConfigModal ? <EditProfile /> : null}
                <img src={ConfigIcon} alt="Icono de un engranaje para editar" />
              </button>
            </div>
            <nav>
              <ul className="flex gap-4 justify-between font-light">
                <li className="">
                  <button aria-selected="true" className="p-2 border-t-2">
                    Reportes
                  </button>
                </li>
                <li>
                  <button className="p-2">Comentarios</button>
                </li>
                <li>
                  <button className="p-2">Favoritos</button>
                </li>
                <li>
                  <button className="p-2">Comentarios Recibidos</button>
                </li>
              </ul>
            </nav>
            <section className="flex flex-col gap-10">
              <ReportCard />
              <ReportCard />
              <ReportCard />
              <ReportCard />
            </section>
          </div>
        </section>
      </MainLayout>
    </div>
  );
}
