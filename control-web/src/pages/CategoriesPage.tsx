import { useEffect } from 'react';
import { useCategoryStore } from '../stores/categoryStore';
import { Table, Button, Form, Input, Select, Typography, message, Card, Tag } from 'antd';
import { CategoryType } from '../types';

const { Title } = Typography;
const { Option } = Select;

export default function CategoriesPage() {
  const { categories, isLoading, fetchCategories, addCategory } = useCategoryStore();
  const [form] = Form.useForm();

  useEffect(() => {
    fetchCategories();
  }, [fetchCategories]);

  const onFinish = async (values: { description: string; type: CategoryType }) => {
    await addCategory(values.description, values.type);
    
    form.resetFields(['description']); 
    message.success('Categoria adicionada com sucesso!');
  };

  const columns = [
    { title: 'ID', dataIndex: 'categoryId', key: 'categoryId', width: '10%' },
    { title: 'Descrição', dataIndex: 'description', key: 'description' },
    {
      title: 'Finalidade',
      dataIndex: 'type',
      key: 'type',
      render: (type: CategoryType) => {
        if (type === CategoryType.Expense) return <Tag color="red">Despesa</Tag>;
        if (type === CategoryType.Income) return <Tag color="green">Receita</Tag>;
        return <Tag color="blue">Ambas</Tag>;
      },
    },
  ];

  return (
    <div>
      <Title level={2}>Gerenciar Categorias</Title>
      
      <Card style={{ marginBottom: 24 }}>
        <Form form={form} layout="inline" onFinish={onFinish} initialValues={{ type: CategoryType.Expense }}>
          <Form.Item name="description" rules={[{ required: true, message: 'Informe a descrição' }]}>
            <Input placeholder="Descrição (ex: Conta de Luz)" maxLength={400} style={{ width: 300 }} />
          </Form.Item>
          
          <Form.Item name="type" rules={[{ required: true, message: 'Selecione a finalidade' }]}>
            <Select style={{ width: 150 }}>
              <Option value={CategoryType.Expense}>Despesa</Option>
              <Option value={CategoryType.Income}>Receita</Option>
              <Option value={CategoryType.Both}>Ambas</Option>
            </Select>
          </Form.Item>
          
          <Form.Item>
            <Button type="primary" htmlType="submit" loading={isLoading}>
              Adicionar
            </Button>
          </Form.Item>
        </Form>
      </Card>

      <Table
        dataSource={categories}
        columns={columns}
        rowKey="categoryId"
        loading={isLoading}
      />
    </div>
  );
}