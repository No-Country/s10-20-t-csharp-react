export function CommentCard({ userProfileImg }) {
  return (
    <article className="flex gap-4 p-4 border-b-2">
      <div>
        <img src={userProfileImg} alt="Foto de perfil" width={40} height={40} />
      </div>
      <div className="flex flex-col gap-2">
        <div className="flex gap-4 items-center">
          <h3 className="font-bold ">Nombre de usuario</h3>
          <small>1 hora</small>
        </div>
        <p>
          <span className="text-terciary-100">@Lorena</span> hizo un comentario
        </p>
      </div>
    </article>
  );
}
