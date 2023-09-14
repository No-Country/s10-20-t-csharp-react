import axios from "axios";
import { createContext, useContext, useEffect, useState } from "react";

function useCommentsReceivedSource() {
  const [commentsReceived, setCommentsReceived] = useState([]);

  const token = localStorage.getItem("userSession");

  const config = {
    headers: { Authorization: `Bearer ${token}` },
  };

  useEffect(() => {
    if (!token) return;
    axios
      .get("https://s10nc.somee.com/api/me/comments/received", config)
      .then(res => setCommentsReceived(res.data.data))
      .catch(err => console.error(err));
  }, []);

  return commentsReceived;
}

const CommentsReceivedContext = createContext();

export function useCommentsReceived() {
  return useContext(CommentsReceivedContext);
}

export const CommentsReceivedProvider = ({ children }) => {
  return (
    <CommentsReceivedContext.Provider value={useCommentsReceivedSource()}>
      {children}
    </CommentsReceivedContext.Provider>
  );
};
