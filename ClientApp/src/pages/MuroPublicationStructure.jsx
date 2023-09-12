import React, { useState } from "react";
import MarkUnreadChatAltIcon from "@mui/icons-material/MarkUnreadChatAlt";
import FavoriteBorderIcon from "@mui/icons-material/FavoriteBorder";
import ShareIcon from "@mui/icons-material/Share";
import { useContext } from "react";
import { UserContext } from "../store/userContext";
import axios from "axios";

const MuroPublicationStructure = ({ pub }) => {
  const userCtx = useContext(UserContext);
  const [commentText, setCommentText] = useState("");
  const [changeColor, setChangeColor] = useState(true);
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
        setChangeColor(false);
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
      <div className="bg-white border-slate-700 card w-96 shadow-2xl shadow-side-left">
        <div className="card-body">
          <div className="flex">
            <div className="avatar">
              <div className="w-8 rounded-full">
                <img src="https://img.freepik.com/foto-gratis/cara-modelo-guapa_23-2147647710.jpg" />
              </div>
            </div>
            <p className="text-gray-400 ml-6">{pub.userName}</p>
          </div>
          <div className=" ml-4">
            <p className="font-bold text-sm color-black">{pub.title}</p>
            <p className="justify-center  text-xs mr-4">{pub.text}</p>

            <div className="mt-2 ">
              <p className=" text-xs mr-4">{pub.district_Name}</p>
              <p className=" text-xs mr-4 underline cursor-pointer">
                Ver en Mapa
              </p>
            </div>
          </div>
          <div className="flex">
            <div className="avatar">
              <div className="w-24 rounded">
                <img src="https://www.elcivismo.com.ar/imagenes/fotos/32495.jpg" />
              </div>
            </div>

            <div className="avatar">
              <div className="w-24 rounded ml-4">
                <img src="https://cdn1.eldia.com/112018/1542172077037.jpg?&cw=630" />
              </div>
            </div>
          </div>
          <div className="flex justify-between">
            {changeColor ? (
              <button
                className="p-2"
                onClick={() => savePublicationInFavs(pub)}
              >
                <FavoriteBorderIcon />
              </button>
            ) : (
              <button className="p-2">
                <FavoriteBorderIcon
                  style={{ color: "white", background: "red" }}
                />
              </button>
            )}
            <button className="p-2" onClick={() => openModalThree()}>
              <MarkUnreadChatAltIcon />
            </button>
            <button className="p-2" onClick={() => openModalFour()}>
              <ShareIcon />
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
  );
};

export default MuroPublicationStructure;
