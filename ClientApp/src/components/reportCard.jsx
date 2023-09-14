import { useState } from "react";

import { LikeIcon } from "../components/atoms/corazon";

import LocationIcon from "../assets/icons/ubicacion2.svg";
import ComentarioIcon from "../assets/icons/comentario.svg";
import CompartirIcon from "../assets/icons/compartir.svg";
import { Link } from "react-router-dom";
import { LikeBorder } from "./atoms/corazonBorder";

export const ReportCard = ({
  profileImg,
  userName,
  title,
  description,
  location,
  likesCount,
  category,
  comments,
  createdAt,
  photoAddress,
}) => {
  const [like, setLike] = useState(false);
  const [share, setShare] = useState(false);
  const [comment, setComment] = useState(false);

  return (
    <div className="p-4 flex flex-col justify-center gap-4 border-b-2">
      <div className="flex gap-8 justify-between">
        <div className="flex gap-4">
          <div>
            <img
              className="rounded-2xl"
              width="50"
              height="50"
              src={profileImg}
              alt="Foto de perfil"
            />
          </div>
          <div className="flex flex-col gap-2">
            <header className="flex flex-col gap-1">
              <small className="text-slate-700">{userName}</small>
              <h3 className="w-[25ch] text-black">
                <strong>{title}</strong>
              </h3>
              <p className="font-light">{description}</p>
            </header>
            <div className="flex flex-col gap-3">
              <div className="flex gap-1 items-center">
                <img
                  className="rounded-2xl"
                  width="20"
                  height="20"
                  src={LocationIcon}
                  alt="Icono de localización en un mapa"
                />
                <p className="text-slate-700">{location}</p>
              </div>
              <a
                href="https://www.google.com/maps/place/La+Plata,+Provincia+de+Buenos+Aires/@-34.9184228,-57.9582372,14.21z/data=!4m6!3m5!1s0x95a2e62b1f0085a1:0xbcfc44f0547312e3!8m2!3d-34.9204948!4d-57.9535657!16zL20vMDIzYjk3?entry=ttu"
                className="text-terciary-100 hover:text-terciary-50 transition-colors"
                target="_blank"
                referrerPolicy="no-referrer"
              >
                Ver en mapa
              </a>
            </div>
            <div className="flex gap-2 w-60">
              <img
                src={photoAddress}
                alt="Foto del reporte"
                className="rounded-xl"
              />
            </div>
            <ul className="flex gap-8 justify-between">
              <li>
                <button
                  onClick={() => setLike(!like)}
                  className="flex items-center gap-2 text-black"
                >
                  {likesCount}{" "}
                  <LikeIcon fill={`${like ? "#df2b2b" : <LikeBorder />}`} />
                </button>
              </li>
              <li>
                <button className="flex items-center gap-2 text-black">
                  {comments}
                  <img
                    width="30"
                    height="30"
                    src={ComentarioIcon}
                    alt="Ícono para comentario"
                  />
                </button>
              </li>
              <li>
                <button className="text-black flex items-center">
                  {" "}
                  <img
                    width="30"
                    height="30"
                    src={CompartirIcon}
                    alt="Ícono para compartir"
                  />
                </button>
              </li>
            </ul>
          </div>
        </div>
        <small className="text-slate-500">{createdAt} hs.</small>

        <div className="flex flex-col gap-2">
          <Link to="/report" className="text-black">
            Ir a mis reportes
          </Link>
          <p className="p-2 border border-terciary-100 text-terciary-100 text-center rounded-md">
            {category}
          </p>
        </div>
      </div>
    </div>
  );
};
