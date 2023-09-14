import { useState, useContext } from "react";
import axios from "axios";
import { Link } from "react-router-dom";
import MarkUnreadChatAltIcon from "@mui/icons-material/MarkUnreadChatAlt";
import FavoriteBorderIcon from "@mui/icons-material/FavoriteBorder";
import ShareIcon from "@mui/icons-material/Share";

import { UserContext } from "../store/userContext";
import { LikeIcon } from "../components/atoms/corazon";
import { LikeBorder } from "../components/atoms/corazonBorder";

import ComentarioIcon from "../assets/icons/comentario.svg";
import CompartirIcon from "../assets/icons/compartir.svg";

const MuroPublicationStructure = ({ pub }) => {
  const userCtx = useContext(UserContext);
  const [commentText, setCommentText] = useState("");
  const [like, setLike] = useState(false);
  const [message, setMessage] = useState("");
  const [showMessage, setShowMessage] = useState(false);

  function openModalThree() {
    const modal = document.getElementById("my_modal_3");
    modal.showModal();
  }

  function openModalFour() {
    const modal = document.getElementById("my_modal_4");
    modal.showModal();
  }

  const sendNewComment = pub => {
    const newComment = {
      text: commentText,
    };
    axios
      .post(
        `https://s10nc.somee.com/api/Quejas/${pub.complaint_ID}/comments`,
        newComment
      )

      .then(res => {
        console.log(res.data);
      })
      .catch(err => {
        console.log(err);
      });
  };

  const savePublicationInFavs = pub => {
    axios
      .put(`https://s10nc.somee.com/api/Quejas/${pub.complaint_ID}/MeGusta`)
      .then(res => {
        console.log(res.data);
        console.log("Enviando Favs");
        setMessage("Almacenado en favoritos");
        setTimeout(() => {
          setShowMessage(true);
        }, 500);
        setTimeout(() => {
          setShowMessage(false);
        }, 2500);
      })
      .catch(err => {
        console.log(err);
      });
  };

  return (
    <div>
      <div className="bg-white border-slate-700 card w-[30em] shadow-2xl shadow-side-left">
        <div className="flex flex-col gap-4 justify-center p-4">
          <div className="flex gap-2">
            <div className="flex">
              <div className="w-8 rounded-full">
                <img
                  className="rounded-full"
                  src={pub.userPhoto}
                  alt="Foto de perfil del usuario"
                />
              </div>
            </div>
            <div className="flex flex-col gap-4">
              <div className="flex flex-col gap-2 w-[40ch]">
                <p className="text-slate-700">{pub.userName}</p>
                <h3 className="font-bold text-black">{pub.title}</h3>
                <p className="text-black">{pub.text}</p>

                <div className="mt-2 ">
                  <p className="text-black">{pub.district_Name}</p>
                  <Link className=" underline cursor-pointer">Ver en Mapa</Link>
                </div>
              </div>
              <div className="flex">
                <div className="avatar">
                  <div className="w-24 rounded-xl">
                    <img src={pub.photoAdress} />
                  </div>
                </div>
              </div>
              <div className="flex gap-5">
                <button
                  onClick={() => {
                    setLike(!like);
                    savePublicationInFavs(pub);
                  }}
                  className="flex items-center gap-2 text-black"
                >
                  {pub.likesCount ?? 0}
                  <LikeIcon fill={`${like ? "#df2b2b" : <LikeBorder />}`} />
                </button>
                <button
                  className="p-2 text-black flex gap-2"
                  onClick={() => openModalThree()}
                >
                  {pub.comments.length ?? 0}
                  <img
                    width="30"
                    height="30"
                    src={ComentarioIcon}
                    alt="Ícono para comentario"
                  />
                </button>
                <button className="p-2" onClick={() => openModalFour()}>
                  <img
                    width="30"
                    height="30"
                    src={CompartirIcon}
                    alt="Ícono para compartir"
                  />
                </button>
              </div>
              <div className="text-center mt-2 justify-center">
                {showMessage ? (
                  <span className="text-sm font-bold">{message}</span>
                ) : null}
              </div>

              <dialog id="my_modal_3" className="modal">
                <form method="dialog" className="modal-box">
                  <button className="btn btn-sm btn-circle btn-ghost absolute right-2 top-2">
                    ✕
                  </button>
                  <div className="flex items-center space-x-2">
                    <div className="avatar">
                      <div className="w-8 rounded-full">
                        <img src={userCtx.userProfileImage} />
                      </div>
                      <p className="ml-2 text-gray-500 text-sm">
                        {userCtx.userName}
                      </p>
                    </div>
                  </div>
                  <textarea
                    className="mt-2 border border-gray-400 w-full rounded-xl text-sm text-center"
                    placeholder="Escribi tu respuesta.."
                    onChange={e => setCommentText(e.target.value)}
                  />
                  <div className="flex justify-end">
                    <button
                      className="btn bg-blue-900 text-white hover:text-blue-900 hover:bg-yellow-400 border text-xs w-18 rounded-xl"
                      onClick={() => sendNewComment(pub)}
                    >
                      Responder
                    </button>
                  </div>
                </form>
              </dialog>

              <dialog id="my_modal_4" className="modal">
                <form method="dialog" className="modal-box w-80">
                  <button className="btn btn-sm btn-circle btn-ghost absolute right-2 top-2">
                    ✕
                  </button>
                  <h3 className="font-bold text-sm flex justify-start">
                    Compartir reclamo en mi muro
                  </h3>
                  <div className="justify-center text-center items-center">
                    <button className="btn mt-2 bg-blue-900 text-white hover:text-blue-900 hover:bg-yellow-400 border text-xs w-18 rounded-xl">
                      Compartir
                    </button>
                  </div>
                </form>
              </dialog>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default MuroPublicationStructure;
