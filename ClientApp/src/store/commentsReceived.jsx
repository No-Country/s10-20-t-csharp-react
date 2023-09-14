import axios from "axios";
import { createContext, useContext, useEffect, useState } from "react";

function useCommentsReceivedSource() {
  const [commentsReceived, setCommentsReceived] = useState([]);
  useEffect(() => {
    axios
      .get("https://s10nc.somee.com/api/me/comments/received", {
        withCredentials: true,
      })
      .then(res => setCommentsReceived(res.data.data));
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
