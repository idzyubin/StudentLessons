import React, { useState } from "react";
import { userAPI } from "../redux/services/userApi";
import { IUser } from "../types/IUser";

const UserCreateForm = () => {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [login, setLogin] = useState("");
  // eslint-disable-next-line no-empty-pattern
  const [createPost, {}] = userAPI.useCreateUserMutation();

  const handleCreate = async () => {
    const user: IUser = { id: 0, firstName, lastName, login };
    await createPost(user);
  };

  return (
    <div>
      <p>Новый пользователь:</p>
      <div>
        <p>Имя</p>
        <input onChange={(e) => setFirstName(e.target.value)} />
      </div>
      <div>
        <p>Фамилия</p>
        <input onChange={(e) => setLastName(e.target.value)} />
      </div>
      <div>
        <p>Email</p>
        <input onChange={(e) => setLogin(e.target.value)} />
      </div>
      <div>
        <button onClick={handleCreate}>Добавить пользователя</button>
      </div>
    </div>
  );
};

export default UserCreateForm;
