import { create } from "zustand";
import api from "../services/api";
import type { User } from "../types";

interface UserState {
    users: User[];
    isLoading: boolean;
    error: string | null;
    fetchUsers: () => Promise<void>;
    addUser: (name: string, age: number) => Promise<void>;
    editUser: (id: number, name: string, age: number) => Promise<void>;
    deleteUser: (id: number) => Promise<void>;
}

export const useUserStore = create<UserState>((set) => ({
    users: [],
    isLoading: false,
    error: null,

    fetchUsers: async  () => {
        set({ isLoading: true, error: null });
        try {
            const response = await api.get("/users");
            set({ users: response.data, isLoading: false });
        } catch (error) {
            set({ error: 'Erro ao buscar usuários', isLoading: false });
        }
    },

    addUser: async (name: string, age: number) => {
        set({ isLoading: true, error: null });
        try {
            const response = await api.post('/users', { name, age });
            set((state) => ({ users: [...state.users, response.data], isLoading: false }));
        } catch (error) {
            set({ error: 'Erro ao criar usuário', isLoading: false });
        }
    }, 

    editUser: async (id: number, name: string, age: number) => {
        set({ isLoading: true, error: null });
        try {
            const response = await api.put(`/users/${id}`, { name, age });
            set((state) => ({
                users: state.users.map(user => user.userId === id ? response.data : user),
                isLoading: false,
                }));
        } catch (error) {
            set({ error: 'Erro ao editar usuário', isLoading: false });
        }
    },

    deleteUser: async (id: number) => {
        set({ isLoading: true, error: null });
        try {
            await api.delete(`/users/${id}`);
            set((state) => ({ users: state.users.filter(user => user.userId !== id), isLoading: false }));
        } catch (error) {
            set({ error: 'Erro ao deletar usuário', isLoading: false });
        }
    }
}));