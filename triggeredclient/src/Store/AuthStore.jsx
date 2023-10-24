import { create } from "zustand";

export const userStore = create((set) => ({
  user: '',
  titles:[],
  setUser: (user)=> set(()=>({user:user})),
  removeUser: ()=> set(()=> ({user:''}))

})) 
