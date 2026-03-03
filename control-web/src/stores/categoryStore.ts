import { create } from "zustand";
import api from "../services/api";
import type { Category, CategoryType } from "../types";

interface CategoryState {
    categories: Category[];
    isLoading: boolean;
    error: string | null;
    fetchCategories: () => Promise<void>;
    addCategory: (description: string, type: CategoryType) => Promise<void>;
}

export const useCategoryStore = create<CategoryState>((set) => ({
    categories: [],
    isLoading: false,
    error: null,

    fetchCategories: async () => {
        set({ isLoading: true, error: null });
        try {
            const response = await api.get('/Categories');
            set({ categories: response.data, isLoading: false });
        } catch (error) {
            set({ error: 'Erro ao buscar categorias', isLoading: false });
        }
    },

    addCategory: async (description: string, type: CategoryType) => {
    set({ isLoading: true, error: null });
    try {
      const response = await api.post('/Categories', { description, type });
      set((state) => ({ 
        categories: [...state.categories, response.data], 
        isLoading: false 
      }));
    } catch (error) {
      set({ error: 'Erro ao criar categoria', isLoading: false });
    }
  }
}))