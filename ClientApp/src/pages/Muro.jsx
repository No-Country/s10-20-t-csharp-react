import React, { useState } from "react";
import { useEffect } from "react";
import axios from "axios";
import MuroPublicationStructure from "./MuroPublicationStructure";
import FiltrosMuro from "./FiltrosMuro";
import Navbar from "../components/navbar";
import { useContext } from "react";
import { UserContext } from "../store/userContext";

const Muro = () => {
  const [allPublications, setAllPublications] = useState([]);
  const [load, setLoad] = useState(true);
  const userCtx = useContext(UserContext);

  useEffect(() => {
    axios
      .get(
        "https://s10nc.somee.com/api/Quejas?SortColumn=CreatedAt&SortOrder=DESC&PageIndex=1&PageSize=10"
      )
      .then(res => {
        console.log(res.data.data);
        setAllPublications(res.data.data);
        setTimeout(() => {
          setLoad(false);
        }, 1500);
      })
      .catch(err => {
        console.log(err);
      });
  }, []);

  useEffect(() => {
    console.log(userCtx.userName);
    console.log(userCtx.userEmail);
    console.log(userCtx.userProfileImage);
  }, []);

  return (
    <div>
      <Navbar />

      {load ? (
        <div className="flex flex-grow h-screen justify-center ">
          <span className="loading loading-spinner loading-lg"></span>
        </div>
      ) : (
        <div className="flex justify-center mt-6 py-6">
          <div className="flex">
            <div className="flex justify-center h-screen mr-6 mt-14">
              <FiltrosMuro />
            </div>

            <div className="flex flex-col items-left">
              <div className="flex justify-center items-center gap-2">
                <p className="text-black">
                  Publicaciones cercanas a tu ubicación
                </p>
                <div className="dropdown">
                  <label tabIndex={0} className="btn m-1">
                    Ordenar Por
                  </label>
                  <ul
                    tabIndex={0}
                    className="dropdown-content z-[1] menu p-2 shadow bg-base-100 rounded-box w-52"
                  >
                    <li>
                      <a>Mas Recientes</a>
                    </li>
                    <li>
                      <a>Mas Antiguos</a>
                    </li>
                  </ul>
                </div>
              </div>

              <div className="flex flex-col gap-6">
                {allPublications.map(p => (
                  <div className="flex flex-col gap-4" key={p.complaint_ID}>
                    <MuroPublicationStructure pub={p} />
                  </div>
                ))}
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default Muro;
