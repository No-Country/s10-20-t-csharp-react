import { useContext, useEffect, useReducer, useState } from "react";
import { Link } from "react-router-dom";
import axios from "axios";

import { MainLayout } from "../layout/MainLayout";
import { LikeIcon } from "../components/atoms/corazon";
import { CommentCard } from "../components/CommentCard";

import { UserContext } from "../store/userContext";
import FotoCalle from "../assets/subir.png";
import ConfigIcon from "../assets/icons/configuracion.svg";
import ProfileImg from "../assets/icons/profile_img.png";
import LocationIcon from "../assets/icons/ubicacion2.svg";
import ComentarioIcon from "../assets/icons/comentario.svg";
import CompartirIcon from "../assets/icons/compartir.svg";
import { ComplainsContext } from "../store/complainsContext";

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
              <p className="font-light">Descripción del reporte</p>
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
          <p className="p-2 border text-primary-100 text-center rounded-md">
            Acera
          </p>
        </div>
      </div>
    </div>
  );
};

export function MyProfile() {
  const userContext = useContext(UserContext);
  const complainsContext = useContext(ComplainsContext);

  const [elems, dispatch] = useReducer(
    (state, action) => {
      switch (action.type) {
        case "setReports":
          return { ...state, elems: action.payload };
        case "setComments":
          return { ...state, elems: action.payload };
        case "setFavorites":
          return { ...state, elems: action.payload };
        case "setReceivedComments":
          return { ...state, elems: action.payload };
        default:
          return <ReportCard />;
      }
    },
    {
      elems: [<ReportCard />],
    }
  );

  const handleClick = e => {};

  return (
    <div>
      <MainLayout>
        <section className="pt-16 pb-20">
          <div className="w-[45em] mx-auto border border-slate-400 shadow-sm p-4 flex flex-col gap-8 font-normal rounded-2xl">
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
                    <strong>{userContext.userName ?? "Nombre"}</strong>
                  </h3>
                  <p className="font-light">Descripción personal</p>
                </header>
              </div>
              <Link to="/editprofile">
                <img src={ConfigIcon} alt="Icono de un engranaje para editar" />
              </Link>
            </div>
            <nav>
              <ul className="flex gap-4 justify-between font-light">
                <li className="">
                  <button
                    aria-selected="true"
                    className="p-2 border-t-2"
                    onClick={e =>
                      dispatch({
                        type: "setReports",
                        payload: [<ReportCard />, <ReportCard />],
                      })
                    }
                  >
                    Mis publicaciones
                  </button>
                </li>
                <li>
                  <button
                    className="p-2"
                    onClick={() =>
                      dispatch({
                        type: "setComments",
                        payload: [<CommentCard />, <CommentCard />],
                      })
                    }
                  >
                    Comentarios
                  </button>
                </li>
                <li>
                  <button
                    className="p-2"
                    onClick={() =>
                      dispatch({
                        type: "setFavorites",
                        payload: [],
                      })
                    }
                  >
                    Favoritos
                  </button>
                </li>
                <li>
                  <button
                    className="p-2"
                    onClick={() =>
                      dispatch({
                        type: "setReceivedComments",
                        payload: [],
                      })
                    }
                  >
                    Comentarios Recibidos
                  </button>
                </li>
              </ul>
            </nav>
            <ul className="flex flex-col gap-10">
              {elems.elems.length > 0 ? (
                elems.elems.map((elem, i) => <li key={i}>{elem}</li>)
              ) : (
                <p className="text-slate-700">No hay elementos</p>
              )}
            </ul>
          </div>
        </section>
      </MainLayout>
    </div>
  );
}
