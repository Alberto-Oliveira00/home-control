import api from "../services/api";
import { create } from "zustand";
import type { Transaction, TransactionType } from "../types";
import { isAxiosError } from "axios";

interface TransactionState {
    transactions: Transaction[];
    isLoading: boolean;
    error: string | null;
    fetchTransactions: () => Promise<void>;
    addTransaction: (description: string, value: number, type: TransactionType, categoryId: number, userId: number) => Promise<boolean>;
}

export const useTransactionStore = create<TransactionState>((set) => ({
    transactions: [],
    isLoading: false,
    error: null,

    fetchTransactions: async () => {
        set({ isLoading: true, error: null });
        try {
            const response = await api.get('/Transaction');
            set({ transactions: response.data, isLoading: false });
        } catch (error) {
            set({ error: 'Erro ao buscar transações', isLoading: false });            
        }
    },

    addTransaction: async (description, value, type, categoryId, userId) => {
        set({ isLoading: true, error: null });
        try {
            const response = await api.post('/Transaction', { description, value, type, categoryId, userId });
            set((state) => ({ 
                transactions: [...state.transactions, response.data],
                isLoading: false,
                error: null,
            }));
            return true;
        } catch (error) {
            let errorMessage = 'Erro ao criar transação';
            if (isAxiosError(error) && error.response?.data?.message) {
                errorMessage = error.response.data.message;
            }
            set({ error: errorMessage, isLoading: false });
            return false;
        }
    }

}))