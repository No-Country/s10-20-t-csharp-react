import { useContext, useReducer, useState } from "react";
import { Link } from "react-router-dom";

import { MainLayout } from "../layout/MainLayout";
import { LikeIcon } from "../components/atoms/corazon";
import { CommentCard } from "../components/CommentCard";

import { UserContext } from "../store/userContext";
import { useComplains } from "../store/complainsContext";
import ConfigIcon from "../assets/icons/configuracion.svg";
import LocationIcon from "../assets/icons/ubicacion2.svg";
import ComentarioIcon from "../assets/icons/comentario.svg";
import CompartirIcon from "../assets/icons/compartir.svg";

const ReportCard = ({
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
        <div className="flex gap-2">
          <img
            className="w-8 h-8 rounded-2xl"
            width="20"
            height="20"
            src={profileImg}
            alt="Foto de perfil"
          />
          <div className="flex flex-col gap-4">
            <header className="flex flex-col gap-2">
              <small>{userName}</small>
              <h3 className="w-[25ch]">
                <strong>{title}</strong>
              </h3>
              <p className="font-light">{description}</p>
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
              <img src={photoAddress} alt="Foto del usuario" />
            </div>
            <ul className="flex gap-8 justify-between">
              <li>
                <button onClick={() => setLike(!like)} className="flex gap-2">
                  {likesCount}{" "}
                  <LikeIcon fill={`${like ? "#df2b2b" : "#111"}`} />
                </button>
              </li>
              <li>
                <button className="flex gap-2">
                  {comments}
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
        <small className="text-slate-500">{createdAt} hs.</small>

        <div className="flex flex-col gap-2">
          <Link to="/report">Ir a mis reportes</Link>
          <p className="p-2 border text-primary-100 text-center rounded-md">
            {category}
          </p>
        </div>
      </div>
    </div>
  );
};

export function MyProfile() {
  const userContext = useContext(UserContext);
  const complains = useComplains();

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
          return <ReportCard title={complainsContext.title} />;
      }
    },
    {
      elems: complains,
    }
  );

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
                  src={userContext.picture_Url}
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
                        payload: [""],
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
                        payload: [""],
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
                        payload: [""],
                      })
                    }
                  >
                    Comentarios Recibidos
                  </button>
                </li>
              </ul>
            </nav>
            <ul className="flex flex-col gap-10">
              {elems.elems.complains.length > 0 ? (
                elems.elems.complains.map(complain => (
                  <li key={complain.title}>
                    <ReportCard
                      title={complain.title}
                      userName={complain.userName}
                      profileImg={complain.userPhoto}
                      likesCount={complain.likesCount ?? 0}
                      category={complain.category_Name}
                      location={complain.district_Name}
                      comments={complain.comments.length}
                      createdAt={new Date(complain.createdAt)
                        .getHours()
                        .toString()}
                      photoAddress={complain.photoAdress}
                    />
                  </li>
                ))
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
