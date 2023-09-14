import { useContext, useReducer, useState } from "react";
import { Link } from "react-router-dom";

import { MainLayout } from "../layout/MainLayout";

import { UserContext } from "../store/userContext";
import { useComplains } from "../store/complainsContext";
import ConfigIcon from "../assets/icons/configuracion.svg";

import { ReportCard } from "../components/reportCard";
import { useComments } from "../store/commentsContext";
import { useFavorites } from "../store/favoritesContext";
import { useCommentsReceived } from "../store/commentsReceived";

export function MyProfile() {
  const userContext = useContext(UserContext);
  const commentsContext = useComments();
  const favoritesContext = useFavorites();
  const commentsReceivedContext = useCommentsReceived();
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

  const [which, setWhich] = useState({
    publicaciones: true,
    comentarios: false,
    favoritos: false,
    comentariosRecibidos: false,
  });

  return (
    <div>
      <MainLayout>
        <section className="pt-16 pb-20">
          <div className="w-[30em] sm:w-full mx-auto border border-slate-400 shadow-sm p-4 flex flex-col gap-8 font-normal rounded-2xl">
            <div className="flex gap-8 items-center ">
              <div className="relative flex justify-center items-center">
                <img
                  className="rounded-full"
                  width="80"
                  height="80"
                  src={userContext.userProfileImage}
                  alt="Foto de perfil"
                />
              </div>
              <div className="flex flex-col gap-4">
                <header>
                  <h3 className="text-black">
                    <strong>{userContext.userName ?? "Nombre"}</strong>
                  </h3>
                  <p className="font-light">{userContext.userDescription}</p>
                </header>
              </div>
              <Link to="/editprofile">
                <img src={ConfigIcon} alt="Icono de un engranaje para editar" />
              </Link>
            </div>
            <nav>
              <ul className="flex sm:flex-col sm:items-center gap-4 sm:gap-2 justify-between font-light">
                <li className="">
                  <button
                    aria-selected="true"
                    className={`p-2 ${
                      which.publicaciones ? "border-t-2" : ""
                    } text-black sm:text-sm`}
                    onClick={e => {
                      setWhich({
                        publicaciones: true,
                        comentarios: false,
                        comentariosRecibidos: false,
                        favoritos: false,
                      });
                      dispatch({
                        type: "setReports",
                        payload: [<ReportCard />, <ReportCard />],
                      });
                    }}
                  >
                    Mis publicaciones
                  </button>
                </li>
                <li>
                  <button
                    className={`p-2 ${
                      which.comentarios ? "border-t-2" : ""
                    } text-black sm:text-sm`}
                    onClick={() => {
                      setWhich({
                        publicaciones: false,
                        comentarios: true,
                        comentariosRecibidos: false,
                        favoritos: false,
                      });
                      dispatch({
                        type: "setComments",
                        payload: [""],
                      });
                    }}
                  >
                    Comentarios
                  </button>
                </li>
                <li>
                  <button
                    className={`p-2 ${
                      which.favoritos ? "border-t-2" : ""
                    } text-black sm:text-sm`}
                    onClick={() => {
                      setWhich({
                        publicaciones: false,
                        comentarios: false,
                        comentariosRecibidos: false,
                        favoritos: true,
                      });
                      dispatch({
                        type: "setFavorites",
                        payload: [""],
                      });
                    }}
                  >
                    Favoritos
                  </button>
                </li>
                <li>
                  <button
                    className={`p-2 ${
                      which.comentariosRecibidos ? "border-t-2" : ""
                    } text-black sm:text-sm`}
                    onClick={() => {
                      setWhich({
                        publicaciones: false,
                        comentarios: false,
                        comentariosRecibidos: true,
                        favoritos: false,
                      });
                      dispatch({
                        type: "setReceivedComments",
                        payload: [""],
                      });
                    }}
                  >
                    Comentarios Recibidos
                  </button>
                </li>
              </ul>
            </nav>
            <ul className="flex flex-col gap-10">
              {which.publicaciones ? (
                elems.elems.complains?.length > 0 ? (
                  elems.elems.complains.map(complain => (
                    <li key={complain.title}>
                      <ReportCard
                        title={complain.title}
                        userName={complain.userName}
                        description={complain.description}
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
                  <p className="text-slate-700 sm:text-center">
                    No hay elementos
                  </p>
                )
              ) : which.comentarios ? (
                commentsContext?.length > 0 ? (
                  JSON.stringify(commentsContext)
                ) : (
                  <p className="text-slate-700 sm:text-center">
                    No hay comentarios
                  </p>
                )
              ) : which.favoritos ? (
                favoritesContext?.length > 0 ? (
                  JSON.stringify(favoritesContext)
                ) : (
                  <p className="text-slate-700 sm:text-center">
                    No hay favoritos
                  </p>
                )
              ) : which.comentariosRecibidos ? (
                commentsReceivedContext?.length > 0 ? (
                  JSON.stringify(commentsReceivedContext)
                ) : (
                  <p className="text-slate-700 sm:text-center">
                    No hay comentarios
                  </p>
                )
              ) : null}
            </ul>
          </div>
        </section>
      </MainLayout>
    </div>
  );
}
